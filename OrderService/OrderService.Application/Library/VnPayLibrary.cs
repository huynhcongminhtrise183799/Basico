﻿using Microsoft.AspNetCore.Http;
using OrderService.Application.DTOs.Vnpay;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Library
{
	public class VnPayLibrary
	{
		private readonly SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());
		private readonly SortedList<string, string> _responseData = new SortedList<string, string>(new VnPayCompare());

		public PaymentResponseModel GetFullResponseData(IQueryCollection collection, string hashSecret)
		{
			var vnPay = new VnPayLibrary();
			foreach (var (key, value) in collection)
			{
				if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
				{
					vnPay.AddResponseData(key, value);
				}
			}

			var vnPayTranId = vnPay.GetResponseData("vnp_TransactionNo");
			var vnpResponseCode = vnPay.GetResponseData("vnp_ResponseCode");
			var vnpSecureHash = collection.FirstOrDefault(k => k.Key == "vnp_SecureHash").Value;
			var orderInfo = vnPay.GetResponseData("vnp_OrderInfo"); // e.g., "BOOKING:xxxx-xxx..."

			var checkSignature = vnPay.ValidateSignature(vnpSecureHash, hashSecret);
			if (!checkSignature)
			{
				return new PaymentResponseModel
				{
					Success = false
				};
			}

			// Parse orderInfo to extract type and id
			var parts = orderInfo.Split(':');
			var isBooking = parts.Length > 0 && parts[0].ToUpper() == "BOOKING";
			var targetId = parts.Length > 1 && Guid.TryParse(parts[1], out var parsedId) ? parsedId : Guid.Empty;
            var accountId = parts.Length > 2 && Guid.TryParse(parts[2], out var parsedAccountId) ? parsedAccountId : (Guid?)null;
            var vnpAmountRaw = vnPay.GetResponseData("vnp_Amount");

			return new PaymentResponseModel
			{
				Success = vnpResponseCode == "00",
				PaymentMethod = PaymentMethod.VNPAY,
				OrderDescription = orderInfo,
				PaymentId = vnPayTranId,
				Amount = double.TryParse(vnpAmountRaw, out var amount) ? amount / 100 : 0,
				TransactionId = vnPayTranId,
				Token = vnpSecureHash,
				VnPayResponseCode = vnpResponseCode,
				IsBooking = isBooking,
				TargetId = targetId,
				Status = vnpResponseCode == "00" ? PaymentStatus.Completed : PaymentStatus.Failed,
                AccountId = accountId
            };
		}


		public string GetIpAddress(HttpContext context)
		{
			var ipAddress = string.Empty;
			try
			{
				var remoteIpAddress = context.Connection.RemoteIpAddress;

				if (remoteIpAddress != null)
				{
					if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
					{
						remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList
							.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
					}

					if (remoteIpAddress != null) ipAddress = remoteIpAddress.ToString();

					return ipAddress;
				}
			}
			catch (Exception ex)
			{
				return ex.Message;
			}

			return "127.0.0.1";
		}

        public void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _requestData[key] = value;
            }
        }


        public void AddResponseData(string key, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				_responseData.Add(key, value);
			}
		}

		public string GetResponseData(string key)
		{
			return _responseData.TryGetValue(key, out var retValue) ? retValue : string.Empty;
		}
		public string CreateRequestUrl(string baseUrl, string vnpHashSecret)
		{
			var data = new StringBuilder();

			foreach (var (key, value) in _requestData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
			{
				data.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(value) + "&");
			}

			var querystring = data.ToString();

			baseUrl += "?" + querystring;
			var signData = querystring;
			if (signData.Length > 0)
			{
				signData = signData.Remove(data.Length - 1, 1);
			}

			var vnpSecureHash = HmacSha512(vnpHashSecret, signData);
			baseUrl += "vnp_SecureHash=" + vnpSecureHash;

			return baseUrl;
		}


		public bool ValidateSignature(string inputHash, string secretKey)
		{
			var rspRaw = GetResponseData();
			var myChecksum = HmacSha512(secretKey, rspRaw);
			return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
		}
		private string HmacSha512(string key, string inputData)
		{
			var hash = new StringBuilder();
			var keyBytes = Encoding.UTF8.GetBytes(key);
			var inputBytes = Encoding.UTF8.GetBytes(inputData);
			using (var hmac = new HMACSHA512(keyBytes))
			{
				var hashValue = hmac.ComputeHash(inputBytes);
				foreach (var theByte in hashValue)
				{
					hash.Append(theByte.ToString("x2"));
				}
			}

			return hash.ToString();
		}

		private string GetResponseData()
		{
			var data = new StringBuilder();
			if (_responseData.ContainsKey("vnp_SecureHashType"))
			{
				_responseData.Remove("vnp_SecureHashType");
			}

			if (_responseData.ContainsKey("vnp_SecureHash"))
			{
				_responseData.Remove("vnp_SecureHash");
			}

			foreach (var (key, value) in _responseData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
			{
				data.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(value) + "&");
			}

			//remove last '&'
			if (data.Length > 0)
			{
				data.Remove(data.Length - 1, 1);
			}

			return data.ToString();
		}
	}
}
public class VnPayCompare : IComparer<string>
{
	public int Compare(string x, string y)
	{
		if (x == y) return 0;
		if (x == null) return -1;
		if (y == null) return 1;
		var vnpCompare = CompareInfo.GetCompareInfo("en-US");
		return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
	}
}


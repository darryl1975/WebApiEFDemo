using BusinessServices.Interfaces;
using BusinessServices.Services;
using EFDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
		private readonly IAccountServices _accountServices;
		public AccountController()
        {
			_accountServices = new AccountServices();

		}

		[HttpGet]
		public IActionResult GetAccountsForOwner(Guid ownerId, [FromQuery] AccountParameters parameters)
		{
			var accounts = _accountServices.GetAccountsByOwner(ownerId, parameters);

			var metadata = new
			{
				accounts.TotalCount,
				accounts.PageSize,
				accounts.CurrentPage,
				accounts.TotalPages,
				accounts.HasNext,
				accounts.HasPrevious
			};

			Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

			//_logger.LogInfo($"Returned {accounts.TotalCount} accounts from database.");

			return Ok(accounts);
		}

		[HttpGet("{id}")]
		public IActionResult GetAccountForOwner(Guid ownerId, Guid id)
		{
			var account = _accountServices.GetAccountByOwner(ownerId, id);

			if (account == null)
			{
				//_logger.LogError($"Account with id: {id}, hasn't been found in db.");
				return NotFound();
			}

			return Ok(account);
		}
	}
}

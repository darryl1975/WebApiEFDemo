using AutoMapper;
using BusinessServices.Interfaces;
using BusinessServices.Services;
using EFDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerServices _ownerServices;
		private readonly IUrlHelper _urlHelper;

		public OwnerController()
        {
			_ownerServices = new OwnerServices();
        }

		public OwnerController(IUrlHelper injectedUrlHelper)
		{
			_ownerServices = new OwnerServices();
			_urlHelper = injectedUrlHelper;
		}


		[HttpGet]
		public IActionResult GetOwners([FromQuery] OwnerParameters ownerParameters)
		{
			if (!ownerParameters.ValidYearRange)
			{
				return BadRequest("Max year of birth cannot be less than min year of birth");
			}

			var owners = _ownerServices.GetOwners(ownerParameters);

			var metadata = new
			{
				owners.TotalCount,
				owners.PageSize,
				owners.CurrentPage,
				owners.TotalPages,
				owners.HasNext,
				owners.HasPrevious
			};

			Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

			//_logger.LogInfo($"Returned {owners.TotalCount} owners from database.");

			return Ok(owners);
		}

		[HttpGet("{id}", Name = nameof(GetOwnerById))]
		public IActionResult GetOwnerById(Guid id)
		{
			var owner = _ownerServices.GetOwnerById(id);

			if (owner == null)
			{
				//_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
				return NotFound();
			}
			else
			{
                //_logger.LogInfo($"Returned owner with id: {id}");
                return Ok(owner);
            }
        }

		[HttpPost]
		public IActionResult CreateOwner([FromBody] Owner owner)
		{
			if (owner == null)
			{
				//_logger.LogError("Owner object sent from client is null.");
				return BadRequest("Owner object is null");
			}

			if (!ModelState.IsValid)
			{
				//_logger.LogError("Invalid owner object sent from client.");
				return BadRequest("Invalid model object");
			}

			Guid newGuid = _ownerServices.CreateOwner(owner);

			return CreatedAtRoute("OwnerById", new { id = newGuid }, owner);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateOwner(Guid id, [FromBody] Owner owner)
		{
			if (owner == null)
			{
				//_logger.LogError("Owner object sent from client is null.");
				return BadRequest("Owner object is null");
			}

			if (!ModelState.IsValid)
			{
				//_logger.LogError("Invalid owner object sent from client.");
				return BadRequest("Invalid model object");
			}

			var dbOwner = _ownerServices.GetOwnerById(id);
			if (dbOwner == null)
			{
                //_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                return NotFound();
			}

			bool succcess = _ownerServices.UpdateOwner(id, owner);
			return Ok(succcess);
			//return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteOwner(Guid id)
		{
			var owner = _ownerServices.GetOwnerById(id);
			if (owner == null)
			{
				//_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
				return NotFound();
			}

			bool success = _ownerServices.DeleteOwner(id);
			return Ok(success);
			//return NoContent();
		}
		
		private OwnerDto CreateLinksForUser(OwnerDto user)
		{
			var idObj = new { id = user.Id };

			user.Links.Add(
				new Link(_urlHelper.Link(nameof(this.GetOwnerById), idObj),
				"self",
				"GET"));

			user.Links.Add(
				new Link(_urlHelper.Link(nameof(this.UpdateOwner), idObj),
				"update_user",
				"PUT"));

			user.Links.Add(
				new Link(_urlHelper.Link(nameof(this.DeleteOwner), idObj),
				"delete_user",
				"DELETE"));

			return user;
		}
	}
}

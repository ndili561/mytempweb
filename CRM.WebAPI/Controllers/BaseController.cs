using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers
{
    public class BaseController<T> : Controller where T : BaseEntity
    {
        protected BaseEntity SavedEntity;
        protected async Task<IActionResult> DeleteAsync(Func<Task<bool>> existFunc, Func<Task<bool>> deleteFunc)
        {
            if (!await existFunc.Invoke())
            {
                return NotFound();
            }

            if (await deleteFunc.Invoke())
            {
                return StatusCode(StatusCodes.Status204NoContent,"Item is deleted successfully.");
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        protected async Task<IActionResult> GetSingleAsync<T>(Func<Task<T>> getFunc)
        {
            var obj = await getFunc.Invoke();

            return obj != null ? (IActionResult)Ok(obj) : NotFound();
        }

        protected async Task<IActionResult> SaveAsync(Func<Task<int>> saveFunc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = await saveFunc.Invoke();

                if (id > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, id);
                }
                return StatusCode(StatusCodes.Status412PreconditionFailed);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                {
                    return StatusCode(StatusCodes.Status409Conflict, e.Message);
                }
                if (e is ValidationException)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, e.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        protected async Task<IActionResult> SaveAndReturnEntityAsync(Func<Task<T>> saveFunc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                SavedEntity = await saveFunc.Invoke();
                return StatusCode(string.IsNullOrWhiteSpace(SavedEntity.ErrorMessage) 
                    ? StatusCodes.Status201Created : StatusCodes.Status412PreconditionFailed, SavedEntity);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                {
                    return StatusCode(StatusCodes.Status409Conflict, e.Message);
                }
                if (e is ValidationException)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, e.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

    }
}
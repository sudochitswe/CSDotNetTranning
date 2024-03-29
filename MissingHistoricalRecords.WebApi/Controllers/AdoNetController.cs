﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MissingHistoricalRecords.WebApi.Models;
using MissingHistoricalRecords.WebApi.Repository;

namespace MissingHistoricalRecords.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoNetController : ControllerBase
    {
        private readonly AdoNetRepository _adoNet;
        public AdoNetController(AdoNetRepository adoNet)
        {
            _adoNet = adoNet;
        }
        [HttpGet("books")]
        public ActionResult<IEnumerable<BookModel>> GetBooks()
        {
            var books = _adoNet.GetBooks();
            return Ok(books);
        }
        [HttpPost("books")]
        public ActionResult<IEnumerable<BookModel>> CrateBook(BookModel createModel)
        {
            var result = _adoNet.CreateBook(createModel);
            var msg = result > 0 ? "Save success" : "Save fail";
            return Ok(msg);
        }
        [HttpGet("books/{id}")]
        public ActionResult<BookModel> GetBook(int id)
        {
            var book = _adoNet.GetBook(id);
            if (book is null)
            {
                return NotFound("No record found.");
            }
            return Ok(book);
        }
        [HttpPut("books/{id}")]
        public IActionResult UpdateBook(int id, BookModel editModel)
        {
            var book = _adoNet.GetBook(id);
            if (book is null)
            {
                return NotFound("No record found.");
            }
             var result = _adoNet.UpdateBook(id, editModel);
            var msg = result > 0 ? "Update success" : "Update fail";
            return Ok(msg);
        }
        [HttpDelete("books/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _adoNet.GetBook(id);
            if (book is null)
            {
                return NotFound("No record found");
            }
            var result = _adoNet.DeleteBook(book);
            var msg = result > 0 ? "Delete success" : "Delete fail";
            return Ok(msg);
        }
        [HttpGet("books/{id}/contents")]
        public ActionResult<IEnumerable<ContentModel>> GetBookContents(int id, int? pageNo)
        {
            var contents = _adoNet.GetBookContents(id, pageNo);
            return Ok(contents);
        }
        [HttpPost("books/{id}/contents")]
        public IActionResult CreateBookContent(ContentModel createModel)
        {
            var result = _adoNet.CreateContent(createModel);
            var msg = result > 0 ? "Create success" : "Create fail";
            return Ok(msg);
        }
        [HttpPut("books/{id}/contents/{contentId}")]
        public IActionResult UpdateBookContent(int contentId, ContentModel updateModel)
        {
            var content = _adoNet.GetCotent(contentId);
            if (content is null)
            {
                return NotFound("No record found.");
            }
            var result = _adoNet.UpdateContent(contentId, updateModel);
            var msg = result > 0 ? "Update success" : "Update fail";
            return Ok(msg);
        }
        [HttpDelete("books/{id}/contents/{contentId}")]
        public IActionResult DeleteBookContent(int contentId)
        {
            var content = _adoNet.GetCotent(contentId);
            if (content is null)
            {
                return NotFound("No record found.");
            }
            var result = _adoNet.DeleteContent(content);
            var msg = result > 0 ? "Update success" : "Update fail";
            return Ok(msg);
        }
    }
}

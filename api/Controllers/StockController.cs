/* 
Jadi, controller itu untu
1. control semua endpoint api nya.
2. Menangani Request
-Kembalikan Response 404 & 200
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Untuk Async kamu
using api.Data; // Untuk import DBContext
using api.Mappers;
using api.Models; // Import Stock dari Models
using Microsoft.AspNetCore.Http.HttpResults; // Tipe hasil (ga terlalu dipake)
using Microsoft.AspNetCore.Mvc; // Untuk API control mu seperti HttpGet

namespace api.Controllers
{
    [Route("api/stock")] // EndPoint masuk ke api/stock
    [ApiController] // Fitur dasar
    public class StockController : ControllerBase // StockController: Nama Controller // ControlBase: turunan dari Controlbase untuk Web API
    {
        private readonly ApplicationDBContext _context; // "_context" adalah variable atau field digunakan dalam seluruh method di controller
        // ApplicatonDBContext: Nama kelas untuk mengatur koneksi dan operasi database
        // _context: field internal untuk simpan instance (?)
        public StockController(ApplicationDBContext context) // Constructor
        // Constructor menerima APplicationDBContext yg di inject oleh dependency injection
        {
            _context = context; // simpan field context
        }

        [HttpGet] // get (Read) ke api/stock
        public IActionResult GetAll() // IACTIONRESULT: interface yang memungkinkan kamu mengembalikan nilai Ok() atau NotFOund() Error seperti di ID bawah setelah ini
        {
            var stocks = _context.Stocks.ToList()
            .Select(s => s.ToStockDto()); // ambil semua data dari tabel stocks dan convert ke list
            // Bro disini ada .Select itu nanti yang akan memberi repsonse dari semua select dari Dtos
            return Ok(stocks);
        }

        [HttpGet("{id}")] // + id
        public IActionResult GetById([FromRoute] int id) // FromRoute: mengambil nilai ID dari URL
        {
            var stock = _context.Stocks.Find(id); // Find = built in function to search // search by PK

            if (stock == null)
            {
                return NotFound(); // return error
            }

            return Ok(stock.ToStockDto()); // return succes // dan response ToStockDto mu
        }
    }
}
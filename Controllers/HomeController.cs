using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using hr.Data;

namespace hr.Controllers
{
    [Authorize] // ❌ Hanya user login yang bisa akses Home
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Ambil data department & jabatan
            var departments = _context.Departments
                .Select(d => new
                {
                    d.Id,
                    d.Nama_Department,
                    JumlahPegawai = d.Users.Count()
                })
                .Where(d => d.JumlahPegawai > 0)
                .ToList();

            var jabatans = _context.Jabatans
                .Select(j => new
                {
                    j.Id,
                    j.Nama_Jabatan,
                    j.Gaji_Pokok,
                    JumlahPegawai = j.Users.Count()
                })
                .Where(j => j.JumlahPegawai > 0)
                .ToList();

            ViewBag.LabelsDepartment = departments.Select(d => d.Nama_Department).ToList();
            ViewBag.DataDepartment = departments.Select(d => d.JumlahPegawai).ToList();
            ViewBag.LabelsJabatan = jabatans.Select(j => j.Nama_Jabatan).ToList();
            ViewBag.DataJabatan = jabatans.Select(j => j.JumlahPegawai).ToList();

            return View();
        }
    }
}

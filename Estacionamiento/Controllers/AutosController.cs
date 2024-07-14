using ClosedXML.Excel;
using Dapper;
using Estacionamiento.Models;
using Estacionamiento.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Estacionamiento.Controllers
{
    public class AutosController : Controller
    {
        private readonly IRepositorioAuto repositorioAuto;
        private readonly IUsuarios usuarios;
        private readonly IRepositorioEntradaSalida repositorioEntradaSalida;

        public AutosController(IRepositorioAuto repositorioAuto,IUsuarios usuarios,IRepositorioEntradaSalida repositorioEntradaSalida)
        {
            this.repositorioAuto = repositorioAuto;
            this.usuarios = usuarios;
            this.repositorioEntradaSalida = repositorioEntradaSalida;
        }


        public async Task<IActionResult> IndexAutos() {

            var autos = await repositorioAuto.ObtenerAutos();
            return View(autos);
        }


        [HttpGet]
        public async Task<IActionResult> Borrar(int Id)
        {
            var auto = await repositorioAuto.ObtenerAutoPorId(Id);
            return View(auto);

        }

        [HttpPost]
        public async Task<IActionResult> BorraAuto(int Id) {

            await repositorioAuto.BorrarAuto(Id);
            return RedirectToAction("IndexAutos");
        
        }

        [HttpGet]
        public IActionResult CrearAuto()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearAuto(Auto auto) {

            if (!ModelState.IsValid) { 
                return View(auto);
            }

            var existePlacas = await repositorioAuto.ExistePlacas(auto.Placas);

            if (existePlacas) {
                ModelState.AddModelError(nameof(auto.Placas), $"las placas {auto.Placas} ya existe.");

                return View(auto);
            }
            await repositorioAuto.CrearAuto(auto);
            return RedirectToAction("IndexAutos");
        }

        public async Task<IActionResult> IndexEntradasSalidas()
        {

            var entradasSalidas= await repositorioEntradaSalida.ObtenereEntradasSalidas();
            return View(entradasSalidas);
        }


        [HttpGet]
        public IActionResult GenerarReporte()
        {
            return View();
        }

        [HttpGet]
        public async Task<FileResult> ExportarExcel(ReporteDto nombrereporte)
        {

            var reporteEntradasSalidas = await repositorioEntradaSalida.ObtenereEntradasSalidas();


            return GenerarExcel(nombrereporte.nombreReporte, reporteEntradasSalidas);
        }


        private FileResult GenerarExcel(String nombreArchivo,IEnumerable<EntradaSalidaDto> entradaSalidas) { 
        
            DataTable dataTable = new DataTable("EntradasSalidas");
            dataTable.Columns.AddRange(new DataColumn[] {
                new DataColumn("Id"),
                new DataColumn("Placas"),
                new DataColumn("FechaEntrada"),
                new DataColumn("FechaSalida"),
                new DataColumn("PrecioPorMinuto"),
                new DataColumn("PrecioTotal")
            });

            foreach (var entradaSalida in entradaSalidas)
            {
                dataTable.Rows.Add(
                    entradaSalida.Id,
                    entradaSalida.Placas,
                    entradaSalida.FechaEntrada,
                    entradaSalida.FechaSalida,
                    entradaSalida.PrecioPorMinuto,
                    entradaSalida.PrecioTotal
                    );
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);

                using (MemoryStream stream = new MemoryStream()) {
                    wb.SaveAs(stream);

                    return File(stream.ToArray(), 
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nombreArchivo);

                }

            }
        }

        [HttpGet]
        public IActionResult RegistrarEntrada()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearEntrada(EntradaSalida model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existePlacas = await repositorioAuto.ExistePlacas(model.Placas);

            if (!existePlacas)
            {
                ModelState.AddModelError(nameof(model.Placas), $"las placas {model.Placas} no existen.");

                return View(model);
            }
            await repositorioAuto.CrearEntrada(model);

            return RedirectToAction("IndexEntradasSalidas");
        }

        [HttpGet]
        public async Task<IActionResult> RegistrarSalida(int Id)
        {
            var entradaSalida = await repositorioAuto.ObtenerEntradaSalidaPorId(Id);
            return View(entradaSalida);
        }

     
        [HttpPost]
        public async Task<IActionResult> CrearSalida(int Id)
        {

            await repositorioAuto.ActualizarSalida(Id);
            return RedirectToAction("IndexEntradasSalidas");

        }
    }
}

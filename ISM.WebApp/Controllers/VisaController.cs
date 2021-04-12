﻿using ClosedXML.Excel;
using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class VisaController : Controller
    {
        public VisaDAO visaDAO;
        public VisaController(VisaDAO visaDAO)
        {
            this.visaDAO = visaDAO;
        }
        public IActionResult Index(string degreeOrMobility = "", string fullname="", string account="", string entry_port="", DateTime? start_date=null, DateTime? expired_date=null, DateTime? date_entry=null, int page=1)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            bool isAdmin = sessionUser.role_name.Equals("Admin") ? true : false;
            bool haveDegree = isAdmin == true ? true : sessionUser.haveDegree;
            int current_staff_id = sessionUser.user_id;
            if (string.IsNullOrEmpty(degreeOrMobility))
            {
                if (haveDegree)
                {
                    degreeOrMobility = "Degree";
                }
                else
                {
                    degreeOrMobility = "Mobility";
                }
            }
            VisaIndexViewModel visaIndexView = new VisaIndexViewModel();
            visaIndexView.page = page;
            visaIndexView.pageSize = 5;
            visaIndexView.totalPage = PagingUtils.calculateTotalPage(visaDAO.GetTotalVisa(isAdmin,haveDegree,degreeOrMobility,current_staff_id,account,fullname,start_date,expired_date,date_entry,entry_port), visaIndexView.pageSize);
            visaIndexView.visalist = visaDAO.GetVisa(isAdmin, haveDegree, degreeOrMobility, current_staff_id, visaIndexView.page, visaIndexView.pageSize, account, fullname, start_date, expired_date, date_entry, entry_port);
            visaIndexView.account = account;
            visaIndexView.fullname = fullname;
            visaIndexView.entry_port = entry_port;
            visaIndexView.start_date = start_date;
            visaIndexView.expired_date = expired_date;
            visaIndexView.date_entry = date_entry;
            visaIndexView.degreeOrMobility = degreeOrMobility;
            return View("Views/Admin/Visa/Visa.cshtml", visaIndexView);
        }

        public bool Edit(int visa_id, DateTime start_date, DateTime expired_date, DateTime entry_date, string entry_port)
        {
            bool result = visaDAO.editVisa(visa_id,start_date,expired_date,entry_date,entry_port);
            return result;
        }

        public bool CreateOrEdit(int days_before)
        {
            bool result = visaDAO.CreateOrEdit(days_before);
            return result;
        }

        public IActionResult ExportToExcel()
        {
            List<Visa> visaExcel = new List<Visa>();
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            if (sessionUser.role_name.Equals("Staff"))
            {
                visaExcel = visaDAO.GetVisaLettersStaffToExcel(sessionUser.user_id);
            }
            else if (sessionUser.role_name.Equals("Admin"))
            {
                visaExcel = visaDAO.GetVisaLettersAdminToExcel();
            }
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Visa");
                var currentRow = 1;
                ws.Cell(currentRow, 1).Value = "Student Name";
                ws.Cell(currentRow, 2).Value = "Student Email";
                ws.Cell(currentRow, 3).Value = "Entry Port";
                ws.Cell(currentRow, 4).Value = "Entry Date";
                ws.Cell(currentRow, 5).Value = "Start Date";
                ws.Cell(currentRow, 6).Value = "Expire Date";
                for (int i = 1; i < 7; i++)
                {
                    ws.Cell(currentRow, i).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                    ws.Cell(currentRow, i).Style.Fill.SetBackgroundColor(XLColor.AliceBlue);
                    ws.Cell(currentRow, i).Style.Font.Bold = true;
                    ws.Cell(currentRow, i).Style.Font.FontSize = 12;
                    ws.Cell(currentRow, i).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Column(i).Width = 30;
                }

                foreach (var item in visaExcel)
                {
                    currentRow++;
                    ws.Cell(currentRow, 1).Value = item.fullname;
                    ws.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 2).Value = item.email;
                    ws.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 3).Value = String.IsNullOrEmpty(item.entry_port) ? "N/A" : item.entry_port;
                    ws.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 4).Value = item.date_entry.ToString("yyyy-MMM-dd");
                    ws.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 5).Value = item.start_date.ToString("yyyy-MMM-dd");
                    ws.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 6).Value = item.expired_date.ToString("yyyy-MMM-dd");
                    ws.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                }

                using (var stream = new MemoryStream())
                {
                    string excelName = $"Visa-{DateTime.Now.ToString("yyyy-MMM-dd")}.xlsx";
                    wb.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }
            }
        }
    }
}

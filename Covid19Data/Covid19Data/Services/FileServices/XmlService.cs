using ClosedXML.Excel;
using Covid19Data.Domain.Entities;
using Covid19Data.Domain.Services;
using Covid19Data.Resources.Constants;
using Covid19Data.Resources.Enuns;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Covid19Data.Services.FileServices
{
    public class XmlService : IXmlService
    {
        #region Parameters
        private readonly ILogger<XmlService> _logger;
        #endregion

        #region Constructors
        public XmlService(ILogger<XmlService> logger) => _logger = logger;
        #endregion

        #region Methods
        public async Task<byte[]> GetXml(List<DayData> data)
        {
            try
            {
                using var workbook = new XLWorkbook();

                var infectedworksheet = workbook.Worksheets.Add("Infectados");
                var deceasedworksheet = workbook.Worksheets.Add("Perdidos");

                Task taskSheetInfected =
                    Task.Factory.StartNew(() => BuildWorkSheet(ref infectedworksheet, data, PacientStatus.Infected));
                
                Task taskSheetDeceased =
                    Task.Factory.StartNew(() => BuildWorkSheet(ref deceasedworksheet, data, PacientStatus.Deceased));

                await Task.WhenAll(taskSheetInfected, taskSheetDeceased);

                string userName = Environment.UserName;

                if (Directory.Exists($"C:\\Users\\{ userName }\\Desktop"))
                    workbook.SaveAs($"C:\\Users\\{ userName }\\Desktop\\historico_covid.xlsx");

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                
                stream.Position = 0;

                return stream.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }
        #endregion

        #region Aux Methods
        private void BuildWorkSheet(ref IXLWorksheet worksheet, List<DayData> dataList, PacientStatus status)
        {
            var orderedData = dataList.OrderBy(d => d.LastUpdatedAtSource);

            var currentRow = 1;
            var currentColumn = 1;

            #region Header
            worksheet.Cell(currentRow, currentColumn++).Value = "Data";
            worksheet.Cell(currentRow, currentColumn++).Value = "Brasil";

            foreach (var property in typeof(State).GetProperties())
                worksheet.Cell(currentRow, currentColumn++).Value = (string)property.GetValue(null, null);
            #endregion

            #region Content
            foreach (var data in orderedData)
            {
                currentRow++;
                currentColumn = 1;

                worksheet.Cell(currentRow, currentColumn++).Value = data.LastUpdatedAtSource;
                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? data.Infected : data.Deceased;

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.AC)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.AC)?.Select(d => d.Count);
                
                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.AL)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.AL)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.AP)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.AP)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.AM)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.AM)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.BA)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.BA)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.CE)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.CE)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.DF)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.DF)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.ES)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.ES)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.GO)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.GO)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.MA)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.MA)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.MT)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.MT)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.MS)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.MS)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.MG)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.MG)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.PA)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.PA)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.PB)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.PB)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.PR)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.PR)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.PE)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.PE)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.PI)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.PI)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.RJ)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.RJ)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.RN)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.RN)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.RS)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.RS)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.RO)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.RO)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.RR)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.RR)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.SC)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.SC)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.SP)?.Select(d => d.Count) :
                    data.DeceasedByRegion?.Where(d => d.State == State.SP)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.SE)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.SE)?.Select(d => d.Count);

                worksheet.Cell(currentRow, currentColumn++).Value = (status == PacientStatus.Infected) ? 
                    data.InfectedByRegion?.Where(d => d.State == State.TO)?.Select(d => d.Count) : 
                    data.DeceasedByRegion?.Where(d => d.State == State.TO)?.Select(d => d.Count);
            }
            #endregion

            IXLRange range = worksheet.Range(worksheet.FirstCellUsed().Address, worksheet.LastCellUsed().Address);

            range.CreateTable();
        }
        #endregion
    }
}

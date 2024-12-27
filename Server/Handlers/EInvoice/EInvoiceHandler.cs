using EInvoiceDemo.Server.Mappers;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Server.Repositories;
using EInvoiceDemo.Shared.DTOs.EInvoice;

namespace EInvoiceDemo.Server.Handlers;

public class EInvoiceHandler(IEInvoiceRepository einvoiceRepository,
                             IGenericRepository<EInvoiceLine> lineRepository,
                             IGenericRepository<EInvoiceLineTax> lineTaxRepository,
                             EInvoiceMapper mapper)
    : GenericHandler<EInvoice, EInvoiceDto, EInvoicesFilter>(einvoiceRepository, mapper)
    , IEInvoiceHandler
{
    public override async Task Edit(EInvoiceDto dto)
    {
        var einvoice = await einvoiceRepository.GetAsync(dto.Id);
        mapper.UpdateEntityFromDto(dto, einvoice);
        foreach (var lineDto in dto.EInvoiceLines)
        {
            var line = einvoice.EInvoiceLines.FirstOrDefault(c => c.Id == lineDto.Id);
            if (line is not null)
            {
                mapper.UpdateEinvoiceLine(lineDto, line);
                foreach (var lineTaxDto in lineDto.EInvoiceLineTaxes)
                {
                    var lineTax = line.EInvoiceLineTaxes.FirstOrDefault(c => c.Id == lineTaxDto.Id);
                    if (lineTax is not null)
                    {
                        mapper.UpdateEinvoiceLineTax(lineTaxDto, lineTax);
                        lineTaxRepository.Update(lineTax);
                    }
                    else
                    {
                        var newLineTax = mapper.CreateEInvoiceLineTax(lineTaxDto);
                        lineTaxRepository.Add(newLineTax);
                    }
                }

                line.EInvoiceLineTaxes
                    .Where(c => !lineDto.EInvoiceLineTaxes.Any(x => c.Id == x.Id))
                    .ToList().ForEach(lineTaxRepository.Delete);

                lineRepository.Update(line);
            }
            else
            {
                var newLine = mapper.CreateEInvoiceLine(lineDto);
                lineRepository.Add(newLine);
            }
        }

        einvoice.EInvoiceLines
            .Where(c => !dto.EInvoiceLines.Any(x => c.Id == x.Id))
            .ToList().ForEach(lineRepository.Delete);

        einvoiceRepository.Update(einvoice);
        await einvoiceRepository.SaveChangesAsync();
    }
}

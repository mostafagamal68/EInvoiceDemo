using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class Range<T>
{
    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public T From { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public T To { get; set; }
}
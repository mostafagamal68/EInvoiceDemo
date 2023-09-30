﻿namespace EInvoiceDemo.Shared.DTOs;

public class Pagination
{
    public int PageNo { get; set; } = 0;
    public int CurrentPage { get; set; } = 0;
    public int RowsCount { get; set; } = 10;
    public int TotalRows { get; set; } = 0;
    public int PagesCount { get; set; } = 0;
}

﻿using EInvoiceDemo.Client.Configurations;
using EInvoiceDemo.Shared.Helpers;
using System.Linq.Expressions;

namespace EInvoiceDemo.Client.Services;

public class ModalBuilder<T> where T : GeneralComponent
{
    readonly ModalData modal;
    private ModalBuilder()
    {
        modal = new() { ComponentType = typeof(T) };
        modal.Parameters.Add(nameof(GeneralComponent.AsModal), true);
    }
    public ModalBuilder(string title)
        : this() => modal.Title = $"{title} {modal.ComponentType.Name}";

    public ModalBuilder(string title, string icon)
        : this(title) => modal.Icon = icon;

    public ModalBuilder(string title, Func<Task> afterClose)
        : this(title) => modal.AfterClose = afterClose;

    public ModalBuilder(string title, string icon, Func<Task> afterClose)
        : this(title, icon) => modal.AfterClose = afterClose;

    public ModalBuilder<T> AddParameter<TValue>(Expression<Func<T, TValue>> expression, TValue value)
    {
        var memberExpr = expression.GetMemberExpression();
        modal.Parameters.Add(memberExpr.Member.Name, value);
        return this;
    }
    public ModalData Build() => modal;
}

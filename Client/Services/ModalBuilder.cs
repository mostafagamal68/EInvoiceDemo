using EInvoiceDemo.Client.Configurations;
using EInvoiceDemo.Shared.Helpers;
using System.Linq.Expressions;

namespace EInvoiceDemo.Client.Services;

public class ModalBuilder<T> where T : GeneralComponent
{
    readonly ModalData modal;
    private ModalBuilder()
    {
        modal = new() { ComponentType = typeof(T) };
    }
    public ModalBuilder(string title)
        : this() => modal.Title = $"{title} {modal.ComponentType.Name}";

    public ModalBuilder(string title, string icon)
        : this(title) => modal.Icon = icon;

    public ModalBuilder(string title, Func<Task> onClose)
        : this(title) => modal.OnClose = onClose;

    public ModalBuilder(string title, string icon, Func<Task> onClose)
        : this(title, icon) => modal.OnClose = onClose;

    public ModalBuilder<T> AddParameter<TValue>(Expression<Func<T, TValue>> expression, TValue value)
    {
        var memberExpr = expression.GetMemberExpression() ?? throw new ArgumentNullException(nameof(expression));
        modal.Parameters.Add(memberExpr.Member.Name, value);
        return this;
    }
    public ModalData Build() => modal;
}

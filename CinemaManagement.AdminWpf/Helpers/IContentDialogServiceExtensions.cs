using System.Windows;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.Helpers
{
    internal static class IContentDialogServiceExtensions
    {
        public static Task<ContentDialogResult> ShowAsync(
            this IContentDialogService dialogService,
            FrameworkElement dialog,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if (dialog is not ContentDialog trueDialog)
            {
                throw new ArgumentException("Dialog must be of type ContentDialog", nameof(dialog));
            }

            return dialogService.ShowAsync(trueDialog, cancellationToken: cancellationToken);
        }
    }
}

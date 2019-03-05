using System.Windows.Controls;
using System.Windows.Interactivity;

namespace SearchDocWindow.Behavior
{
    /// <summary>
    /// Поведение для GridControl на прокрутку мыши.
    /// </summary>
    public class MouseWheelBehavior : Behavior<DataGrid>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseWheel += AssociatedObject_PreviewMouseWheel;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseWheel -= AssociatedObject_PreviewMouseWheel;
        }

        private void AssociatedObject_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            int currentIndex = AssociatedObject.SelectedIndex;

            if (e.Delta < 0)
            {
                if (currentIndex < AssociatedObject.Items.Count - 1)
                {
                    currentIndex++;
                }
            }
            else
            {
                if (currentIndex > 0)
                {
                    currentIndex--;
                }
            }

            AssociatedObject.SelectedIndex = currentIndex;
        }
    }
}

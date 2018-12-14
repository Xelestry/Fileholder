using System.Collections.Generic;

namespace Fileholder.ViewModel.Templates.DragAndDropMultiSelector
{
    public class MultiSelectorWithSingleValueExtended
    {
        public MultiSelectorWithSingleValueExtended()
        {
            SelectedItems = new List<MultiSelectorWithSingleValueExtendedItem>();
            AvailableItems = new List<MultiSelectorWithSingleValueExtendedItem>();
        }

        public List<MultiSelectorWithSingleValueExtendedItem> SelectedItems { get; set; }
        public List<MultiSelectorWithSingleValueExtendedItem> AvailableItems { get; set; }
    }
}
using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.Components
{
    public partial class Checkbox : ComponentBase
    {
        #region Parameters

        /// <summary>
        /// Indicates whether the checkbox is checked or not
        /// </summary>
        [Parameter]
        public bool Checked { get; set; }

        /// <summary>
        /// The color of the checkbox
        /// </summary>
        [Parameter]
        public string Color { get; set; } = "#dedede";

        /// <summary>
        /// Fires when the value for the control changes
        /// </summary>
        [Parameter]
        public Action<bool>? ValueChanged { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Changes the State of checkbox
        /// </summary>
        private void ToggleState()
        {
            Checked = !Checked;
            ValueChanged?.Invoke(Checked);
        }

        #endregion
    }
}

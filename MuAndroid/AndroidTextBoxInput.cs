using System.Threading.Tasks;
using Client.Main;
using Client.Main.Controls.UI.Common;
using Microsoft.Xna.Framework.Input;

namespace MuAndroid
{
    /// <summary>
    /// Shows the native MonoGame Android text-input dialog for game-rendered
    /// text boxes (e.g. the character name field), which do not poll the
    /// Android soft keyboard themselves.
    /// </summary>
    public static class AndroidTextBoxInput
    {
        public static void Show(TextBoxControl box)
        {
            if (box == null)
                return;

            Task.Run(async () =>
            {
                string result = await KeyboardInput.Show(
                    title: "Character Name",
                    description: box.PlaceholderText,
                    defaultText: box.Text,
                    usePasswordMode: false);

                if (result != null)
                    MuGame.ScheduleOnMainThread(() => box.Text = result);
            });
        }
    }
}

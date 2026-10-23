using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
            var logger = MuGame.AppLoggerFactory?.CreateLogger("AndroidTextBoxInput");
            if (box == null)
            {
                logger?.LogWarning("Show called with null box");
                return;
            }

            logger?.LogInformation("Show called (text length {Length})", box.Text?.Length ?? 0);

            Task.Run(async () =>
            {
                string result = await KeyboardInput.Show(
                    title: "Character Name",
                    description: box.PlaceholderText,
                    defaultText: box.Text,
                    usePasswordMode: false);

                logger?.LogInformation("KeyboardInput.Show returned {Length} chars", result?.Length ?? -1);
                if (result != null)
                    MuGame.ScheduleOnMainThread(() => box.Text = result);
            });
        }
    }
}

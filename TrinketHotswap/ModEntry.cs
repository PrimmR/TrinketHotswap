using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Objects.Trinkets;

namespace TrinketHotswap
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }

        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
        {
            // Ignore if player isn't in control
            if (!Context.CanPlayerMove || !Context.IsWorldReady)
                return;

            // Check right click
            if (Game1.player.CurrentItem is Trinket && e.Button.IsActionButton())
            {
                Trinket held = (Trinket)Game1.player.CurrentItem;
                Trinket? equipped = Game1.player.trinketItems.ElementAtOrDefault(0);
                int idx = Game1.player.getIndexOfInventoryItem(held);

                // Swap held and equipped
                Game1.player.trinketItems.Clear();
                Game1.player.trinketItems.Add(held);
                Game1.player.removeItemFromInventory(held);
                Game1.player.addItemToInventory(equipped, idx); // If equipped null, nothing happens

                Game1.player.playNearbySoundAll("pickUpItem");
            }
        }
    }
}
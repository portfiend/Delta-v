using System.Numerics;
using Content.Client.UserInterface.Controls;
using Content.Shared.DeltaV.QuickPhrase;
using Content.Shared.Movement.Systems;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Prototypes;

namespace Content.Client.DeltaV.AACTablet.UI;

[GenerateTypedNameReferences]
public sealed partial class AACWindow : FancyWindow
{
    private IPrototypeManager _prototypeManager;

    public AACWindow(AACBoundUserInterface ui, IPrototypeManager prototypeManager)
    {
        RobustXamlLoader.Load(this);
        _prototypeManager = prototypeManager;
        CreatePhraseButtons(ui);
    }

    private void CreatePhraseButtons(AACBoundUserInterface ui)
    {
        var spaceWidth = 10;
        var parentWidth = 540;
        var columnCount = 4;

        var paddingSize = spaceWidth * 2;
        var gutterScale = (columnCount - 1) / columnCount;
        var columnWidth = (parentWidth - paddingSize) / columnCount;
        var buttonWidth = columnWidth - spaceWidth * gutterScale;

        var loc = IoCManager.Resolve<ILocalizationManager>();
        var phrases = _prototypeManager.EnumeratePrototypes<QuickPhrasePrototype>();
        foreach (var phrase in phrases)
        {
            var text = loc.GetString(phrase.Text);
            var phraseButton = new Button
            {
                Access = AccessLevel.Public,
                MaxSize = new Vector2(buttonWidth, buttonWidth),
                ClipText = false
            };

            var buttonLabel = new RichTextLabel
            {
                StyleClasses = { "button" },
            };

            buttonLabel.SetMessage(text);
            phraseButton.AddChild(buttonLabel);
            Body.AddChild(phraseButton);
        }
    }
}
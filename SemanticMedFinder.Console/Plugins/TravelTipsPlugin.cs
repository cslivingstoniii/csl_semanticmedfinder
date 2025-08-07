using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace SemanticMedFinder.Plugins;

public class TravelTipsPlugin
{
    [KernelFunction]
    [Description("Provides a useful travel phrase in the destination language.")]
    public string GetTravelPhrase(string phrase)
    {
        return phrase switch
        {
            "hello" => "Hola (Spanish), Bonjour (French), こんにちは (Japanese)",
            "thank you" => "Gracias (Spanish), Merci (French), ありがとう (Japanese)",
            _ => $"Sorry, I don't have a phrase for '{phrase}' yet."
        };
    }
}

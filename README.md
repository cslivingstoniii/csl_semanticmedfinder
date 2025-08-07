# SemanticMedFinder.Console

**SemanticMedFinder.Console** is an AI-powered travel assistant prototype built using .NET 8 and Microsoft’s Semantic Kernel. Developed as part of my advanced AI training, this project demonstrates practical integration of large language models into traditional C# applications using function calling, conversational memory, and plugin-based reasoning.

Through natural language interaction, the assistant can understand user intent, offer personalized travel suggestions, and call modular C# plugins to return helpful content like local phrases or destination summaries—bridging cutting-edge AI with enterprise-ready .NET development.

## Features

- Natural language chat interface via OpenAI GPT-4o
- Kernel function plugins to support tool-calling
- Console-based C# application with semantic memory
- Secure API key management using `.env`
- Vibe-ready for Continue.dev agent workflows

## Tech Stack

- .NET 8 (C#)
- Semantic Kernel
- OpenAI (GPT-4o)
- Visual Studio Code
- Continue.dev (Vibe Coding Agent)

## Getting Started

1. Install [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
2. Clone the repo and navigate to the console project:

   ```bash
   git clone git@github.com:your-username/csl_semanticmedfinder.git
   cd csl_semanticmedfinder/SemanticMedFinder.Console
   ```

3. Add your OpenAI key to the environment:

   Create a `.env` file:
   ```
   OPENAI_API_KEY=sk-...
   ```

4. Run the project:

   ```bash
   dotnet run
   ```

## Example Prompt

```
Tell me about your travel plans.
```

```
Would you like some activity recommendations?
```

```
Would you like some helpful phrases in the local language?
```

## Project Structure

- `Program.cs` - Main console entry with chat and plugin logic
- `Plugins/TravelTipsPlugin.cs` - Sample plugin for tool-calling

## 🧪 Vibe Coding Integration

This project is compatible with [Continue.dev](https://continue.dev) for Vibe-style agent workflows. You can extend it using natural language prompts like:

> “Create a new Semantic Kernel plugin to fetch weather data.”

---

© Steve Livingston, 2025

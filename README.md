
# Terminal AI 🤖

This repository provides a terminal with ai for easily terminal writing. 

## **Interact with the AI:** this is video example using

https://github.com/user-attachments/assets/3668b446-f97c-46f5-855d-24d7f75db039

- Write `exit` in message to exit from AiTerminal
- Write `clear_data` to reset data to use new api config

- If your ai model not there you have to go Program.cs

```c#
public class Program
{
    public static async Task Main(string[] args)
    {
        ApiKeyProvider.SelectedModel = ChatModel.Groq.Meta.Llama38B; // Custom model
        // Other logic
    }
}
```

## **Download It From Releases:**

https://github.com/wisamidris77/AiTerminal/releases

## **Getting Started Developing:**

1. **Clone the repository:**
   ```
   git clone https://github.com/wisamidris77/AiTerminal.git
   ```

2.  **Launch the application:**

    ```
    cd AiTerminal
    dotnet run 
    ```

## **Key Features:**

* **Versatile:** Easily integrate various AI models for now it's only Gemini api.
* **User-Friendly:** Intuitive command-line interface for easy interaction and control.
* **Customizable:** Tailor the AI's behavior and responses to your specific needs.
* **Efficient:** Leverage the power of the terminal for a streamlined and efficient workflow.

**Changelog**
### 0.0.2 Second Version
1. Supporting larger llms apis with llmTornado
2. Making option to clear ai data using `clear_data` in message
3. Making option to exit app using `exit` in message
4. Supporting multiple commands at once
5. Improved prompt

### 0.0.1 Inital Version
1. Inital Version
2. Single command support
3. Only gemini api


Thanks to https://github.com/lofcz/LlmTornado

**Contributing:**

We welcome contributions from the community\! Please feel free to submit bug reports, feature requests, and pull requests.

**License:**

This project is licensed under the MIT License - see the [LICENSE](https://www.google.com/url?sa=E&source=gmail&q=LICENSE) file for details.

**Disclaimer:**

This project is for educational and experimental purposes only. The performance and accuracy of the AI may vary depending on the specific models and tasks. So be careful when you write yes :D

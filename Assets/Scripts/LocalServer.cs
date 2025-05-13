using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

public class LocalServer : MonoBehaviour
{
    private HttpListener listener;
    private bool isServerRunning;

    private void Start()
    {
        DontDestroyOnLoad(gameObject); // Garante que o GameObject persista entre cenas
        StartLocalServer(); // Inicia o servidor local
    }

    private void StartLocalServer()
    {
        if (isServerRunning) return; // Evita inicializações duplicadas

        try
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();
            isServerRunning = true;

            Debug.Log("Servidor local iniciado em http://localhost:8080/");
            Task.Run(() => HandleRequests());
        }
        catch (Exception e)
        {
            Debug.LogError("Erro ao iniciar o servidor local: " + e.Message);
        }
    }

    private async Task HandleRequests()
    {
        while (isServerRunning)
        {
            var context = await listener.GetContextAsync();
            string path = context.Request.Url.AbsolutePath.TrimStart('/');

            // Caminho base para os arquivos servidos na pasta Assets
            string basePath = Application.dataPath;
            string filePath = Path.Combine(basePath, path);

            Debug.Log($"Requisição recebida para: {path}");
            Debug.Log($"Caminho completo do arquivo: {filePath}");

            if (File.Exists(filePath))
            {
                byte[] fileData = File.ReadAllBytes(filePath);
                context.Response.ContentType = GetMimeType(filePath);
                context.Response.ContentLength64 = fileData.Length;
                await context.Response.OutputStream.WriteAsync(fileData, 0, fileData.Length);
            }
            else
            {
                context.Response.StatusCode = 404;
                byte[] error = System.Text.Encoding.UTF8.GetBytes("404 - File Not Found");
                await context.Response.OutputStream.WriteAsync(error, 0, error.Length);
            }

            context.Response.OutputStream.Close();
        }
    }

    private string GetMimeType(string filePath)
    {
        string ext = Path.GetExtension(filePath).ToLower();
        return ext switch
        {
            ".html" => "text/html",
            ".css" => "text/css",
            ".js" => "application/javascript",
            ".txt" => "text/plain",
            ".png" => "image/png",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            _ => "application/octet-stream",
        };
    }

    private void StopLocalServer()
    {
        if (isServerRunning)
        {
            isServerRunning = false;
            listener?.Stop();
            listener?.Close();
            Debug.Log("Servidor local parado.");
        }
    }

    private void OnApplicationQuit()
    {
        StopLocalServer(); // Garante que o servidor será encerrado ao fechar o jogo
    }

    public void OpenTestPage()
    {
        Application.OpenURL("http://localhost:8080/Main.html");
    }
}

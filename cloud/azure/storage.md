# storage

## Blobs: 

Se utilizan para almacenar archivos de gran tamaño, ya sean música, video, imágenes, binarios, etcétera


Contenedores

Un contenedor organiza un conjunto de blobs, de forma parecida a un directorio en un sistema de archivos. Una cuenta de almacenamiento puede contener un número ilimitado de contenedores y un contenedor puede almacenar un número ilimitado de blobs



## Tables

En ocasiones es posible que necesitemos almacenar información estructuradas sin llegar a ser relacional.


## .Net Core

Download Files from Azure with .NET Core Web API and Blazor WebAssembly
https://code-maze.com/download-files-from-azure-with-net-core-web-api-and-blazor-webassembly/#download-web-api

## Librería y conflictos para gestionar archivos en azure file

Para la gestión de archivos en azure file utilizar el ensamblado Microsoft.WindowsAzure.Storage que lo podemos acceder desde el framework Microsoft.NETCore.App (versión utilizada en el proyecto crédito digital 2.1.0), no utilizar el ensamblado Microsoft.Azure.Storage.Common de la librería Microsoft.Azure.Storage.Blob este presenta conflictos de ambiguedad de tipos se agrega url para detalles de conflicto

https://github.com/Azure/azure-storage-net/issues/842

## Fragmentos de código para lectura y escritura de archvos en azure file

```
public CloudFile readFile(ContextBl context, string path)
{
    //Obtenemos la cadena de conexión Azure Storage para la cooperativa
    var azureStorageConnectionString = context.CurrentCooperative.CooperativesParameters
        .Where(p => p.Code.Equals(ParametersCooperativeCode.AzureStorageConnectionString)
        && p.Status).FirstOrDefault();

    if (azureStorageConnectionString == null)
    {
        throw new Exception($"No se encontró el parametro cooperativa: {context.CurrentCooperative.Code} - {ParametersCooperativeCode.AzureStorageConnectionString}");
    }

    //Obtenemos el recurso raiz de azure storage 
    var rootResourceNameAzureFile = context.CurrentCooperative.CooperativesParameters
        .Where(p => p.Code.Equals(ParametersCooperativeCode.RootResourceNameAzureFile)
        && p.Status).FirstOrDefault();

    if (rootResourceNameAzureFile == null)
    {
        throw new Exception($"No se encontró el parametro cooperativa: {context.CurrentCooperative.Code} - {ParametersCooperativeCode.RootResourceNameAzureFile}");
    }            

    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(azureStorageConnectionString.Value);
    CloudFileClient fileClient = cloudStorageAccount.CreateCloudFileClient();
    CloudFileShare fileShare = fileClient.GetShareReference(rootResourceNameAzureFile.Value);

    var rootDirectory = fileShare.GetRootDirectoryReference();
    var file = rootDirectory.GetFileReference(path);

    return file;
}

public async Task<CloudFile> writeFileAsync(ContextBl context, string path, string fileName, byte[] buffer)
{
    //Obtenemos la cadena de conexión Azure Storage para la cooperativa
    var azureStorageConnectionString = context.CurrentCooperative.CooperativesParameters
        .Where(p => p.Code.Equals(ParametersCooperativeCode.AzureStorageConnectionString)
        && p.Status).FirstOrDefault();

    if (azureStorageConnectionString == null)
    {
        throw new Exception($"No se encontró el parametro cooperativa: {context.CurrentCooperative.Code} - {ParametersCooperativeCode.AzureStorageConnectionString}");
    }

    //Obtenemos el recurso raiz de azure storage 
    var rootResourceNameAzureFile = context.CurrentCooperative.CooperativesParameters
        .Where(p => p.Code.Equals(ParametersCooperativeCode.RootResourceNameAzureFile)
        && p.Status).FirstOrDefault();

    if (rootResourceNameAzureFile == null)
    {
        throw new Exception($"No se encontró el parametro cooperativa: {context.CurrentCooperative.Code} - {ParametersCooperativeCode.RootResourceNameAzureFile}");
    }

    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(azureStorageConnectionString.Value);
    CloudFileClient fileClient = cloudStorageAccount.CreateCloudFileClient();
    CloudFileShare fileShare = fileClient.GetShareReference(rootResourceNameAzureFile.Value);

    var rootDirectory = fileShare.GetRootDirectoryReference();
    var subDirectory = rootDirectory.GetDirectoryReference(path);

    //Se crea si no existe toda la jerarquía de carpetas en donde se guardará el archivo
    await CreateRecursiveIfNotExistsAsync(subDirectory);

    var file = rootDirectory.GetFileReference(Path.Combine(path, fileName));

    await file.UploadFromByteArrayAsync(buffer, 0, buffer.Length);

    return file;
}
```

## Herramienta de microsoft para gestionar archivos en azure file
Azure Storage Explorer
Free tool to conveniently manage your Azure cloud storage resources from your desktop
https://azure.microsoft.com/en-us/features/storage-explorer/
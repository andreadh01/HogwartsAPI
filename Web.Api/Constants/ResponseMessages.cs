namespace HogwartsAPI.Web.Api.Constants
{
    public static class ResponseMessages
    {
        // Success Messages
        public const string Success = "Operación completada con éxito.";
        public const string Created = "Recurso creado con éxito.";
        public const string Updated = "Recurso actualizado con éxito.";
        public const string Deleted = "Recurso eliminado con éxito.";

        // Error Messages
        public const string NotFound = "El recurso solicitado no fue encontrado.";
        public const string BadRequest = "Solicitud inválida.";
        public const string Unauthorized = "Acceso no autorizado.";
        public const string Forbidden = "No tienes permiso para realizar esta acción.";
        public const string Conflict = "El recurso ya existe.";
        public const string ServerError = "Ocurrió un error interno del servidor.";
    }
}
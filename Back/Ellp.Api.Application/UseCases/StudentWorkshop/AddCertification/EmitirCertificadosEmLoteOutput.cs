﻿namespace Ellp.Api.Application.UseCases.StudentWorkshop.EmitirCertificadosEmLote
{
    public class EmitirCertificadosEmLoteOutput
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> HashCodes { get; set; } = new List<string>();
    }
}


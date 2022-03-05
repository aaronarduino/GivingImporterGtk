using System;
namespace HCCInfrastructure.Helpers
{
    public static class EnumHelper
    {
        public enum BatchFileStatus
        {
            New,
            Uploaded,
            UploadedWithErrors,
            Error,
            NotFound,
            Hidden,
        }
    }
}

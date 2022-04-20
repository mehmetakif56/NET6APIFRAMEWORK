using TTBS.Core.Entities;

namespace TTBS.Core.Interfaces
{
    public interface ISessionHelper
    {
        UserEntity User { get; set; }

        string BUILD_VERSION { get; }

        string BUILD_DATE { get; }

        string BasePath { get; }

        bool IsPrivilegedUser();
    }
}

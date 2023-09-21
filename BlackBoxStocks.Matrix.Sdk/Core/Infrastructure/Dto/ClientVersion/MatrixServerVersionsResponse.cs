using System.Collections.Generic;

namespace BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.ClientVersion
{
    public class MatrixServerVersionsResponse
    {
        public List<string> versions { get; set; }
        public UnstableFeatures unstable_features { get; set; }
    }
}
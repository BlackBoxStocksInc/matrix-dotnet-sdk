using System;
using System.Collections.Generic;
using BlackBoxStocks.Matrix.Sdk.Core.Domain.RoomEvent;

namespace BlackBoxStocks.Matrix.Sdk
{
    public class MatrixRoomEventsEventArgs : EventArgs
    {
        public MatrixRoomEventsEventArgs(List<BaseRoomEvent> matrixRoomEvents, string nextBatch)
        {
            MatrixRoomEvents = matrixRoomEvents;
            NextBatch = nextBatch;
        }

        public List<BaseRoomEvent> MatrixRoomEvents { get; }
        
        public string NextBatch { get; }
    }
}
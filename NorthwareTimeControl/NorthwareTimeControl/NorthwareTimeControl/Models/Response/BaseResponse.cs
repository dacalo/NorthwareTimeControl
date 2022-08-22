using System.Collections.Generic;

namespace NorthwareTimeControl.Models.Response
{
    public class BaseResponse<T>
    {
        public T DataResponse { get; set; }
        public List<DetailResponse> Details { get; set; }
        public bool Successful { get; set; }
        public DetailResponse AddDetailResponse(int id, string message) => new DetailResponse { Id = id, Message = message };
        public List<string> Errors { get; set; }
    }

    public class DetailResponse
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}

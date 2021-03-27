using System.Collections.Generic;

namespace SGM.Legado.STUR.Api.Dto
{
    public class ResultDto<TResult>
    {
        public int StatusCode { get; set; }
        public TResult Data { get; set; }
        public ICollection<string> Errors { get; set; }

		public ResultDto()
		{
            Errors = new List<string>();

        }

        protected ResultDto(int statusCode, TResult data)
        {
            StatusCode = statusCode;
            Data = data;
        }

        protected ResultDto(int statusCode, params string[] erros)
        {
            StatusCode = statusCode;
            Errors = new List<string>();

            foreach (var erro in erros)
            {
                Errors.Add(erro);
            }
        }

        protected ResultDto(int statusCode, IEnumerable<string> erros)
        {
            StatusCode = statusCode;
            Errors = new List<string>();

            foreach (var erro in erros)
            {
                Errors.Add(erro);
            }
        }

        public static ResultDto<TResult> Success(TResult data)
        {
            return new ResultDto<TResult>(200, data);
        }

        public static ResultDto<TResult> Validation(string msgErro)
        {
            return new ResultDto<TResult>(400, msgErro);
        }

        public static ResultDto<TResult> Validation(IEnumerable<string> msgsErro)
        {
            return new ResultDto<TResult>(400, msgsErro);
        }

        public static ResultDto<TResult> Error(string msgErro)
        {
            return new ResultDto<TResult>(500, msgErro);
        }

        public static ResultDto<TResult> Error(IEnumerable<string> msgsErro)
        {
            return new ResultDto<TResult>(500, msgsErro);
        }
    }
}

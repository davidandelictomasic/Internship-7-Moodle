
namespace MoodleApplication.Application.Common.Model
{
    public abstract class RequestHandler<TRequst, TResult> where TRequst : class where TResult : class

    {
        public Guid RequestId => Guid.NewGuid();

        public async Task<Result<TResult>> ProcessActiveRequestAsnync(TRequst request)
        {
            var result = new Result<TResult>();
            if (await IsAuthorized() == false)
            {
                result.SetUnauthorizedResult();
                return result;
            }
            await HandleRequest(request, result);

            return result;
        }
        protected abstract Task<Result<TResult>> HandleRequest(TRequst request, Result<TResult> result);
        protected abstract Task<bool> IsAuthorized();

    }
}
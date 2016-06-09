using MediatR;

namespace ContosoUniversity.Features.Student
{
    public class Test
    {
        public class Query : IRequest<Result>
        {
            public string Name { get; set; }
        }

        public class Result
        {
            public string Message { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Result>
        {
            public Result Handle(Query message)
            {
                var model = new Result
                {
                    Message = $"Hello {message.Name}"
                };

                return model;
            }
        }
    }
}
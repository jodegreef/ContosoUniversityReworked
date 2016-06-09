using AutoMapper;
using ContosoUniversity.DAL;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Features.Student
{
    public class Create
    {
        public class Command : IRequest
        {
            public string LastName { get; set; }

            [Display(Name = "First Name")]
            public string FirstMidName { get; set; }

            public DateTime? EnrollmentDate { get; set; }
        }

        public class Handler : RequestHandler<Command>
        {
            private readonly SchoolContext _db;
            private readonly IMapper _mapper;

            public Handler(SchoolContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            protected override void HandleCore(Command message)
            {
                var student = _mapper.Map<Command, Models.Student>(message);

                _db.Students.Add(student);
                _db.SaveChanges();
            }
        }
    }
}
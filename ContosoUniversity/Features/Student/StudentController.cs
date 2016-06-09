﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Features.Student
{
    public class StudentController : Controller
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ViewResult Test(Test.Query query)
        {
            var model = _mediator.Send(query);

            return View(model);
        }

       
    }

}
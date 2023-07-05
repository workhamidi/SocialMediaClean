﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace SocialMediaClean.Presentation.Controllers;


[ApiController]
[Route("[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}


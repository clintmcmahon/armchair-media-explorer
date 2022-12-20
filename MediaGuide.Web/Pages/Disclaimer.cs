﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MediaGuide.Web.Pages;

public class DisclaimerModel : PageModel
{
    private readonly ILogger<DisclaimerModel> _logger;

    public DisclaimerModel(ILogger<DisclaimerModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}


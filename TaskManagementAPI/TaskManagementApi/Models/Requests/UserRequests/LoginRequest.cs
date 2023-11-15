﻿using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models.Requests.UserRequests
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;

    }
}

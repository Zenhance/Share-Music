﻿namespace Share_Music.DTOs.Login
{
    public class UserLoginResponseDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty ;
    }
}

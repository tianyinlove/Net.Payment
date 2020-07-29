﻿namespace Alipay.AspNetCore.Parser

{
    public class SignItem
    {
        public string SignSourceDate { get; set; }
        public string Sign { get; set; }

        public override string ToString()
        {
            return "{" +
                "Sign:" + Sign +
                ",SignSourceDate:" + SignSourceDate +
                "}";
        }
    }
}

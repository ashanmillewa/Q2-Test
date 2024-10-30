using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace EmployeeRecordSystem
{
    public interface  IMemoryDB
    {
        void AddRecord(int key, string value);

        void UpdateRecord(int key, string value);


        void DeleteRecord(int key);


        (int, string) FindRecord(int key);
        List<(int, string)> GetAllRecords();
        List<(int Key, string Value)> Search(string stub);




    }
}


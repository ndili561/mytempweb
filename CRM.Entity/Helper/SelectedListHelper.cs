using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CRM.Entity.Model.Customer;
using CRM.Entity.Model.Employee;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Employee;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Helper
{
    public static class SelectedListHelper
    {
        public static List<SelectListItem> GetSelectListForItems(List<BaseLookupDto> items, string selectedItem, bool addPleaseSelect = true)
        {
            var locationsSelectList = GetSelectedListEntity(selectedItem, addPleaseSelect);
            if (items != null && items.Any())
            {
                locationsSelectList.AddRange(items.Where(x => x != null).Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }));
                if (!string.IsNullOrWhiteSpace(selectedItem) && locationsSelectList.Any(x => x.Value == selectedItem))
                {
                    locationsSelectList.First(d => d.Value == selectedItem).Selected = true;
                }
                else if (locationsSelectList.Any(d => d.Value == ""))
                {
                    locationsSelectList.First(d => d.Value == "").Selected = true;
                }

            }
            return locationsSelectList;
        }

        public static List<SelectListItem> GetPersonSelectList(List<PersonDto> model, string selectedItem, bool addPleaseSelect = true)
        {
            var userSelectList = GetSelectedListEntity(selectedItem, addPleaseSelect);
            userSelectList.AddRange(model.Where(x => !string.IsNullOrWhiteSpace(x?.Forename + x?.Surname)).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Forename + " " + x.Surname
            }));
            return userSelectList;
        }
        public static List<SelectListItem> GetUserSelectList(List<UserDto> model, string selectedItem, bool addPleaseSelect = true)
        {
            var userSelectList = GetSelectedListEntity(selectedItem, addPleaseSelect);
            userSelectList.AddRange(model.Where(x => !string.IsNullOrWhiteSpace(x?.Name)).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }));
            return userSelectList;
        }

        public static List<SelectListItem> GetTaskSelectList(List<TaskDto> model, string selectedItem, bool addPleaseSelect = true)
        {
            var taskSelectList = GetSelectedListEntity(selectedItem, addPleaseSelect);
            taskSelectList.AddRange(model.Where(x => !string.IsNullOrWhiteSpace(x?.Name)).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }));
            return taskSelectList;
        }

        private static List<SelectListItem> GetSelectedListEntity(string selectedValue, bool addPleaseSelect = true)
        {
            var selectList = new List<SelectListItem>();
            if (addPleaseSelect)
            {
                selectList.Add(new SelectListItem
                {
                    Value = "",
                    Text = @"    Please Select    ",
                    Disabled = !string.IsNullOrWhiteSpace(selectedValue)
                });
            }
            return selectList;
        }
        public static List<SelectListItem> GetTimeIntervalForCalendar(string selectedTime = "")
        {
            var defaultselected = selectedTime;
            int minuteInterval = 15;
            if (string.IsNullOrWhiteSpace(defaultselected))
            {
                defaultselected = RoundUp(DateTime.Now, TimeSpan.FromMinutes(minuteInterval))
                    .ToString("HH:mm");
            }
            else
            {
                var time = DateTime.Now.ToShortDateString().Substring(0, 10) + " " + selectedTime + ":00";
                var date = DateTime.Parse(time);
                defaultselected = RoundUp(date, TimeSpan.FromMinutes(minuteInterval))
                    .ToString("HH:mm");
            }

            var query =
                Enumerable.Range(0, 24 * 60 / minuteInterval)
                    .Select(i => DateTime.Today.AddHours(24).AddMinutes(i * minuteInterval).ToString("HH:mm"))
                    .ToList();
            var list = (from e in query
                select new SelectListItem
                {
                    Text = e,
                    Value = e,
                    Selected = e == defaultselected
                }).ToList();
            return list;
        }

        public static DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks);
        }

        public static List<SelectListItem> GetApplicationTaskSelectList(List<WebApplicationDto> model, IEnumerable<int> linkedApplicationIds)
        {
            var taskApplicationSelectList = new List<SelectListItem>();
            taskApplicationSelectList.AddRange(model.Where(x => !string.IsNullOrWhiteSpace(x?.Name)).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Selected = linkedApplicationIds.Contains(x.Id),
                Text = x.Name
            }));
            return taskApplicationSelectList;
        }

        public static List<SelectListItem> GetApplicationSelectList(List<WebApplicationDto> model, string selectedItem, bool addPleaseSelect = true)
        {
            var applicationSelectList = GetSelectedListEntity(selectedItem, addPleaseSelect);
            applicationSelectList.AddRange(model.Where(x => !string.IsNullOrWhiteSpace(x?.Name)).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }));
            return applicationSelectList;
        }

        public static List<SelectListItem> GetSelectListForManager(List<UserDto> model, string selectedItem, bool addPleaseSelect = true)
        {
            var applicationSelectList = GetSelectedListEntity(selectedItem, addPleaseSelect);
            applicationSelectList.AddRange(model.Where(x => !string.IsNullOrWhiteSpace(x?.Name)).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }));
            return applicationSelectList;
        }

        public static List<SelectListItem> GetSelectListForUserGroup(List<UserGroupDto> model, string selectedItem, bool addPleaseSelect = true)
        {
            var applicationSelectList = GetSelectedListEntity(selectedItem, addPleaseSelect);
            applicationSelectList.AddRange(model.Where(x => !string.IsNullOrWhiteSpace(x?.Name)).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }));
            return applicationSelectList;
        }

        public static List<SelectListItem> GetApplicationRolesSelectList(List<RoleDto> model, IEnumerable<int> selectedItems, bool addPleaseSelect = true)
        {
            var roleSelectList = new List<SelectListItem>();
            roleSelectList.AddRange(model.Where(x => !string.IsNullOrWhiteSpace(x?.RoleName)).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Selected = selectedItems.Contains(x.Id),
                Text = x.RoleName
            }));
            return roleSelectList;
        }
        public static List<SelectListItem> GetPermissionSelectList(List<MenuItemDto> model, IEnumerable<int> permissionIds)
        {
            var taskApplicationSelectList = new List<SelectListItem>();
            taskApplicationSelectList.AddRange(model.Where(x => !string.IsNullOrWhiteSpace(x?.DisplayName)).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Selected = permissionIds.Contains(x.Id),
                Text = x.DisplayName
            }));
            return taskApplicationSelectList;
        }
    }
}
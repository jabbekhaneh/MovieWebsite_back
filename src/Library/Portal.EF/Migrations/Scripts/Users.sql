INSERT INTO dbo.Permissions
(Id,Title,Name)
VALUES
(NEWID(),'ManagerRoles','مدیریت نقش ها'),
(NEWID(),'ManagerUsers','مدیریت کاربرانی'),
(NEWID(),'ManagerBlogCategory','مدیریت دسته بندی بلاگ ها'),
(NEWID(),'ManagerBlogs','مدیریت بلاگ ها'),
(NEWID(),'Settings','تنظیمات')
/**/
INSERT INTO dbo.Roles
(Id,Title)
VALUES
(NEWID(),'Developer')
/**/


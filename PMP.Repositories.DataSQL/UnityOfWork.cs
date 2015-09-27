using PMP.Core.Entities;
using PMP.Repositories.DataSQL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.Repositories.DataSQL
{
    public class UnityOfWork
    {
        PMPDBEntities _context;
        public UnityOfWork(PMPDBEntities context)
        {
            _context = context;
        }

        private ProjectLogRepository _ProjectLogRepository;
        private ProjectRepository _ProjectRepository;
        private ProjectUserRepository _ProjectUserRepository;
        private SubtaskRepository _SubtaskRepository;
        private TaskCommentRepository _TaskCommentRepository;
        private TaskLogRepository _TaskLogRepository;
        private TaskRepository _TaskRepository;
        private UserRepository _UserRepository;

        public UserRepository UserRepo
        {
            get
            {
                if (_UserRepository == null)
                    _UserRepository = new UserRepository(_context);
                return _UserRepository;
            }
        }
        public TaskRepository TaskRepo
        {
            get
            {
                if (_TaskRepository == null)
                    _TaskRepository = new TaskRepository(_context);
                return _TaskRepository;
            }
        }
        public TaskLogRepository TaskLogRepo
        {
            get
            {
                if (_TaskLogRepository == null)
                    _TaskLogRepository = new TaskLogRepository(_context);
                return _TaskLogRepository;
            }
        }
        public TaskCommentRepository TaskCommentRepo
        {
            get
            {
                if (_TaskCommentRepository == null)
                    _TaskCommentRepository = new TaskCommentRepository(_context);
                return _TaskCommentRepository;
            }
        }
        public SubtaskRepository SubtaskRepo
        {
            get
            {
                if (_SubtaskRepository == null)
                    _SubtaskRepository = new SubtaskRepository(_context);
                return _SubtaskRepository;
            }
        }
        public ProjectUserRepository ProjectUserRepo
        {
            get
            {
                if (_ProjectUserRepository == null)
                    _ProjectUserRepository = new ProjectUserRepository(_context);
                return _ProjectUserRepository;
            }
        }
        public ProjectLogRepository ProjectLogRepo
        {
            get
            {
                if (_ProjectLogRepository == null)
                    _ProjectLogRepository = new ProjectLogRepository(_context);
                return _ProjectLogRepository;
            }
        }
        public ProjectRepository ProjectRepo
        {
            get
            {
                if (_ProjectRepository == null)
                    _ProjectRepository = new ProjectRepository(_context);
                return _ProjectRepository;
            }
        }

    }
}

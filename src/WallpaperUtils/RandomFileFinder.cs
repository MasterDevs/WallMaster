using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WallpaperUtils
{
    public class RandomFileFinder : IEnumerator<string>
    {
        private static Random _rand = new Random(DateTime.Now.Millisecond);

        #region Fields

        private string _current;
        private string _dir;
        private Regex _filterRegex;
        private string[] _filters;
        private bool _includeSubDirectories;
        private bool _needsReset;

        #endregion

        #region Properties

        public string DirectoryPath
        {
            get { return _dir; }
            set
            {
                if (!Directory.Exists(value))
                {
                    throw new System.IO.FileNotFoundException("The path specified does not exist", value);
                }
                _dir = value;
                _needsReset = true;
            }
        }

        public string[] Filter
        {
            get { return _filters; }
            set
            {
                _filters = value;
                createFilterRegex();
                _needsReset = true;
            }
        }

        public bool IncludeSubDirectories
        {
            get { return _includeSubDirectories; }
            set
            {
                _includeSubDirectories = value;
                _needsReset = true;
            }
        }

        #endregion

        #region CTOR

        public RandomFileFinder(string dir, string[] filters, bool includeSubDirectories)
        {
            DirectoryPath = dir;
            Filter = filters;
            IncludeSubDirectories = includeSubDirectories;
            _needsReset = false;

            MoveNext();
        }

        public RandomFileFinder(string dir) : this(dir, null, false) { }

        #endregion

        #region Public Methods

        // IEnumerator Members
        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        // IEnumerator<string> Members
        public string Current
        {
            get { return _current; }
        }

        // IDisposable Members
        public void Dispose() { }

        public bool MoveNext()
        {
            if (_needsReset)
            {
                throw new Exception("Enumerator is no longer valid.  Need to call reset.");
            }

            DirectoryInfo di = new DirectoryInfo(_dir);
            if (di.Exists)
            {
                FileInfo[] files = getFileList(di, _includeSubDirectories);

                _current = getRandomFile(files, _filterRegex);
            }
            else
            {
                _current = null;
            }
            return true;
        }

        public void Reset()
        {
            _needsReset = false;
        }

        private static FileInfo[] getFileList(DirectoryInfo di, bool includeSubDirs)
        {
            FileInfo[] files;

            if (includeSubDirs)
            {
                files = di.GetFiles("*", SearchOption.AllDirectories);
            }
            else
            {
                files = di.GetFiles();
            }

            return files;
        }

        private static IEnumerable<string> getFiltered(FileInfo[] files, string filterList)
        {
            string[] filterArr = filterList.Split(';', ',', ' ', '|');

            foreach (FileInfo fi in files)
            {
                foreach (string filter in filterArr)
                {
                    if (fi.Extension.Equals(filter, StringComparison.CurrentCultureIgnoreCase))
                    {
                        yield return fi.FullName;
                    }
                }
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Creates a regular expression from the filter array
        /// <example>
        /// Input:  (".jpg", ".jpeg", ".bmp")
        /// Output:  (.jpg|.jpeg|.bmp)$
        /// </example>
        /// <param name="filter">An array of file extensions</param>
        /// <returns>The appropriate regular expression</returns>
        private static Regex createFilterRegex(string[] filter)
        {
            Regex r;

            if (filter == null)
            {
                r = new Regex(".");
                return r;
            }
            else
            {
                string f = string.Join("|", filter);
                f = string.Format("({0})$", f);
                r = new Regex(f, RegexOptions.IgnoreCase);
            }
            return r;
        }

        private static string getRandomFile(FileInfo[] files, Regex filter)
        {
            IEnumerable<FileInfo> filtered = files.Where(x => filter.IsMatch(x.Extension));

            List<FileInfo> filteredList = new List<FileInfo>(filtered);

            if (filteredList.Count == 0)
            {
                return null;
            }
            else
            {
                int idx = _rand.Next(0, filteredList.Count);
                return filteredList[idx].FullName;
            }
        }

        /// <summary>
        /// Creates a regular expression from the filter array
        /// <example>
        /// Input:  (".jpg", ".jpeg", ".bmp")
        /// Output:  (.jpg|.jpeg|.bmp)$
        /// </example>
        /// </summary>
        private void createFilterRegex()
        {
            _filterRegex = createFilterRegex(_filters);
        }

        #endregion
    }
}
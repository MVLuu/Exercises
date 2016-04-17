using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList
{
    class UsingArrayList
    {
        ArrayList arrayList;

        public UsingArrayList()
        {
            
        }

        public ArrayList NewContacts(object[] input)
        {
            ArrayList arrayList = new ArrayList();

            foreach(string value in input)
            {
                arrayList.Add(value);
            }

            return arrayList;
        }

        public ArrayList AddContacts(ArrayList arrayList, object[] input)
        {
            foreach(string value in input)
            {
                arrayList.Add(value);
            }

            return arrayList;
        }

        public ArrayList RemoveContacts(ArrayList arrayList, object removeItem)
        {
            arrayList.Remove(removeItem);
            return arrayList;
        }

        public ArrayList UpdateContacts(ArrayList arrayList, object oldValue, object newValue)
        {
            int index = arrayList.IndexOf(oldValue);
            arrayList[index] = newValue;
            return arrayList;
        }

        public void PrintArrayList(ArrayList arrayList)
        {
            foreach(object value in arrayList)
            {
                Console.WriteLine(value);
            }
        }

        public ArrayList DeleteItem(ArrayList arrayList, object itemToRemove)
        {
            arrayList.Remove(itemToRemove);
            return arrayList;
        }
    }
}

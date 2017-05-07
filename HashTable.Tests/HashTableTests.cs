using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashTable.Tests
{
    [TestClass]
    public class HashTableTests
    {
        [TestMethod]
        public void Add_KeyIsNotPresent_ShouldStoreAValueUnderSpecifiedKey()
        {
            var sut = new HashTable();
            var testValue = new object();

            sut.Add("testKey", testValue);

            Assert.AreEqual(testValue, sut["testKey"]);
        }

        [TestMethod]
        public void Add_KeyIsPresent_ShouldThrowInvalidOperation()
        {
            var sut = new HashTable();
            var testValue = new object();
            var testKey = "testKey";
            sut.Add(testKey, testValue);
            Exception exception = null;

            try
            {
                sut.Add(testKey, 1);
            }
            catch (Exception ex)
            {
                exception = ex;             
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.AreEqual("An entry with provided key is already present in this hash table.", exception.Message);
        }

        [TestMethod]
        public void Contains_KeyIsPresent_ShouldReturnTrue()
        {
            var sut = new HashTable();
            var testValue = new object();
            var testKey = "testKey";
            sut.Add(testKey, testValue);

            var contains = sut.Contains(testKey);

            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void Contains_KeyIsNotPresent_ShouldReturnFalse()
        {
            var sut = new HashTable();

            var contains = sut.Contains("testKey");

            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void IndexerGet_KeyIsPresent_ShouldReturnValue()
        {
            var sut = new HashTable();
            var testValue = new object();
            var testKey = "testKey";
            sut.Add(testKey, testValue);

            var value = sut[testKey];

            Assert.AreEqual(testValue, value);
        }

        [TestMethod]
        public void IndexerGet_KeyIsNotPresent_ShouldThrowKeyNotFound()
        {
            var sut = new HashTable();
            var testKey = "testKey";
            Exception exception = null;

            try
            {
                var result = sut[testKey];
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(KeyNotFoundException));            
        }

        [TestMethod]
        public void IndexerSet_KeyIsPresentValueIsNotNull_ShouldSetValue()
        {
            var sut = new HashTable();
            var testValue = new object();
            var newValue = 1;
            var testKey = "testKey";
            sut.Add(testKey, testValue);

            sut[testKey] = newValue;

            Assert.AreEqual(newValue, sut[testKey]);
        }

        [TestMethod]
        public void IndexerSet_KeyIsNotPresentValueIsNotNull_ShouldStoreAValueUnderSpecifiedKey()
        {
            var sut = new HashTable();
            var newValue = new object();
            var testKey = "testKey";

            sut[testKey] = newValue;

            Assert.AreEqual(newValue, sut[testKey]);
        }

        [TestMethod]
        public void IndexerSet_KeyIsPresentValueIsNull_ShouldRemoveSpecifiedKey()
        {
            var sut = new HashTable();
            var testValue = new object();
            var testKey = "testKey";
            sut.Add(testKey, testValue);

            sut[testKey] = null;

            Assert.IsFalse(sut.Contains(testKey));
        }

        [TestMethod]
        public void TryGet_KeyIsPresent_ShouldReturnTrueAndassignAValue()
        {
            var sut = new HashTable();
            var testValue = new object();
            var testKey = "testKey";
            sut.Add(testKey, testValue);
            object result;

            var contains = sut.TryGet(testKey, out result);

            Assert.IsTrue(contains);
            Assert.AreEqual(testValue, result);
        }

        [TestMethod]
        public void TryGet_KeyIsNotPresent_ShouldReturnFalse()
        {
            var sut = new HashTable();
            object result;

            var contains = sut.TryGet("testKey", out result);

            Assert.IsFalse(contains);
            Assert.IsNull(result);
        }
    }
}

using System.Threading;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;
using SniffCore.Controls;

namespace SniffCore.Layouting.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class TitledItemsControlTests
    {
        [SetUp]
        public void Setup()
        {
            _target = new TestableTitledItemsControl();
        }

        private TestableTitledItemsControl _target;

        [Test]
        public void IsItemItsOwnContainerOverride_CalledWithTextBlock_ReturnsFalse()
        {
            var element = new TextBlock();

            var result = _target.CallIsItemItsOwnContainerOverride(element);

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsItemItsOwnContainerOverride_CalledWithTitledItem_ReturnsTrue()
        {
            var element = new TitledItem();

            var result = _target.CallIsItemItsOwnContainerOverride(element);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GetContainerForItemOverride_Called_ReturnsANewTitledItem()
        {
            var result = _target.CallGetContainerForItemOverride();

            Assert.That(result, Is.InstanceOf<TitledItem>());
        }

        private class TestableTitledItemsControl : TitledItemsControl
        {
            public bool CallIsItemItsOwnContainerOverride(object item)
            {
                return base.IsItemItsOwnContainerOverride(item);
            }

            public DependencyObject CallGetContainerForItemOverride()
            {
                return base.GetContainerForItemOverride();
            }
        }
    }
}
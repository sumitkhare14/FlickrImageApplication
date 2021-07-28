using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using FlickrImageApplication;
using FlickrImageApplication.ViewModel;
using ImagingService.Interfaces;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Rhino.Mocks;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace FlickrImageApplicationUnitTest
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class FlickrImageApplicationTest
    {
        [SetUp]
        public void Setup()
        {
            _container = new UnityContainer();
            _imagingService = MockRepository.GenerateMock<IImagingService>();
            _container.RegisterInstance(_imagingService);
        }

        private IUnityContainer _container;
        private IImagingService _imagingService;

        [Test]
        public void TestSearchImageCommand()
        {
            //Arrange
            var viewModel = _container.Resolve<FlickrUiViewModel>();

            //Act
            viewModel.SearchString = "Trees";
            viewModel.UiModel.PhotoCollection=new ObservableCollection<string>{"data1","data2","data3"};
            viewModel.ClearSearchResultsCommand.Execute(null);

            //Assert
            Assert.IsTrue(string.IsNullOrEmpty(viewModel.SearchString));
            Assert.IsTrue(viewModel.UiModel.PhotoCollection.Count==0);
        }

        [Test]
        public void TestCanExecuteCommand()
        {
            //Arrange
            var viewModel = _container.Resolve<FlickrUiViewModel>();

            //Act
            viewModel.SearchString = "";

            //Assert
            Assert.IsFalse(viewModel.SearchImageCommand.CanExecute(null));
        }
    }
}
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightController.Test
{

   
    public class LightActuator_ActuatateLights_Tests
    {

        [Fact]
        public void MotionDetected_SetCurrentDate()
        {
            //Arrange
            bool motionDetected = true;
            DateTime startTime = new DateTime(2000, 1, 1); //not now

            Mock<ILightSwitcher> lightSwitcherMock = new();
            var ioc = new AutoMocker();

            //Act
            LightActuator lightActuator = ioc.CreateInstance<LightActuator>();
            lightActuator.LastMotionTime = startTime;
            lightActuator.ActuateLights(motionDetected);
            DateTime lastMotionTime = lightActuator.LastMotionTime;


            //Assert 
            lastMotionTime.Should().NotBe(startTime);

        }

        [Fact]
        public void MotionNotDetected_SetCurrentDate()
        {
            //Arrange
            bool motionDetected = false;        
            DateTime startTime = new DateTime(2000, 1, 1); //not now
            var ioc = new AutoMocker();

            //Act
            Mock<ILightSwitcher> lightSwitcherMock = new();
            LightActuator lightActuator = ioc.CreateInstance<LightActuator>();

            lightActuator.LastMotionTime = startTime;
            lightActuator.ActuateLights(motionDetected);
            DateTime lastMotionTime = lightActuator.LastMotionTime;


            //Assert 
            lastMotionTime.Should().Be(startTime);

        }

        [Theory]
        [InlineData(TimePeriod.Morning, false )]
        [InlineData(TimePeriod.Afternoon, false )]
        [InlineData(TimePeriod.Evening, true )]
        [InlineData(TimePeriod.Night, true )]
        public void MotionDetected_TurnOn(TimePeriod timeOfDay, bool expected)
        {
            //Arrange
            bool motionDetected = true;
            bool actualTurnOn = false;

            var ioc = new AutoMocker();
            Mock<ILightSwitcher> lightSwitcherMock = ioc.GetMock<ILightSwitcher>();
            Mock<ITimePeriodHelper> timePeriodHelperMock = ioc.GetMock<ITimePeriodHelper>();
            timePeriodHelperMock.Setup(m => m.GetTimePeriod(It.IsAny<DateTime>())).Returns(timeOfDay);
            lightSwitcherMock.Setup(m => m.TurnOn()).Callback(() => actualTurnOn = true);


            //Act
            LightActuator lightActuator = ioc.CreateInstance<LightActuator>();
            lightActuator.ActuateLights(motionDetected);

            //Assert 
            actualTurnOn.Should().Be(expected);

        }

        [Theory]
        [InlineData(TimePeriod.Morning, true)]
        [InlineData(TimePeriod.Afternoon, true)]
        [InlineData(TimePeriod.Evening, false)]
        [InlineData(TimePeriod.Night, false)]
        public void MotionNotDetected_TurnOff(TimePeriod timeOfDay, bool expected)
        {
            //Arrange
            bool motionDetected = false;
            bool actualTurnOff = false;
            var startTime = DateTime.Now.AddSeconds(-1); //less than a minute ago

            var ioc = new AutoMocker();
            Mock<ILightSwitcher> lightSwitcherMock = ioc.GetMock<ILightSwitcher>();
            Mock<ITimePeriodHelper> timePeriodHelperMock = ioc.GetMock<ITimePeriodHelper>();
            timePeriodHelperMock.Setup(m => m.GetTimePeriod(It.IsAny<DateTime>())).Returns(timeOfDay);
            lightSwitcherMock.Setup(m => m.TurnOff()).Callback(() => actualTurnOff = true);


            //Act
            LightActuator lightActuator = ioc.CreateInstance<LightActuator>();
            lightActuator.LastMotionTime = startTime; 

            lightActuator.ActuateLights(motionDetected);

            //Assert 
            actualTurnOff.Should().Be(expected);

        }

        [Theory]
        [InlineData(61, true)]
        [InlineData(59, false)]
        public void MotionNotDetectedLongEnough_TurnOff(int seconds, bool expected)
        {
            //Arrange
            bool motionDetected = false;
            bool actualTurnOff = false;
            var startTime = DateTime.Now.AddSeconds(-1 * seconds); //less than a minute ago

            var timeOfDay = TimePeriod.Night;

            var ioc = new AutoMocker();
            Mock<ILightSwitcher> lightSwitcherMock = ioc.GetMock<ILightSwitcher>();
            Mock<ITimePeriodHelper> timePeriodHelperMock = ioc.GetMock<ITimePeriodHelper>();
            timePeriodHelperMock.Setup(m => m.GetTimePeriod(It.IsAny<DateTime>())).Returns(timeOfDay);
            lightSwitcherMock.Setup(m => m.TurnOff()).Callback(() => actualTurnOff = true);


            //Act
            LightActuator lightActuator = ioc.CreateInstance<LightActuator>();
            lightActuator.LastMotionTime = startTime;

            lightActuator.ActuateLights(motionDetected);

            //Assert 
            actualTurnOff.Should().Be(expected);

        }



    }

}

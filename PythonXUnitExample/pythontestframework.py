class TestCase:
    def __init__(self, name):
        self.name = name
    
    def setUp(self):
        pass
    
    def tearDown(self):
        pass
    
    def run(self, result):
        result.testStarted()
        try:
            self.setUp()
            method = getattr(self, self.name)
            method()
        except:
            result.testFailed()
        self.tearDown()

class WasRun(TestCase):
    def __init__(self, name):
        TestCase.__init__(self, name)
    
    def setUp(self):
        self.log = "setUp "
    
    def tearDown(self):
        self.log += "tearDown "
    
    def testMethod(self):
        self.wasRun = 1
        self.log += "testMethod "
    
    def testBrokenMethod(self):
        raise Exception

class BadSetUp(TestCase):
    def __init__(self, name):
        TestCase.__init__(self, name)
    
    def setUp(self):
        raise Exception
    
    def testMethod(self):
        pass

class TestCaseTest(TestCase):
    def setUp(self):
        self.result = TestResult()
    
    def testTemplateMethod(self):
        self.test = WasRun("testMethod")
        self.test.run(self.result)
        assert(self.test.log == "setUp testMethod tearDown ")
    
    def testResult(self):
        test = WasRun("testMethod")
        test.run(self.result)
        assert("1 run, 0 failed" == self.result.summary())
    
    def testFailedResultFormatting(self):
        self.result.testStarted()
        self.result.testFailed()
        assert("1 run, 1 failed" == self.result.summary())
    
    def testFailedResult(self):
        test = WasRun("testBrokenMethod")
        test.run(self.result)
        assert("1 run, 1 failed" == self.result.summary())
    
    def testFailedSetUp(self):
        test = BadSetUp("testMethod")
        test.run(self.result)
        assert("1 run, 1 failed" == self.result.summary())
    
    def testSuite(self):
       suite = TestSuite()
       suite.add(WasRun("testMethod"))
       suite.add(WasRun("testBrokenMethod"))
       suite.run(self.result)
       assert("2 run, 1 failed" == self.result.summary())
    
class TestResult:
    def __init__(self):
        self.runCount = 0
        self.errorCount = 0
    
    def testStarted(self):
        self.runCount += 1
    
    def testFailed(self):
        self.errorCount += 1
    
    def summary(self):
        return "%d run, %d failed" % (self.runCount, self.errorCount)

class TestSuite:
    def __init__(self):
        self.tests = []
    
    def add(self, test):
        self.tests.append(test)
    
    def run(self, result):
        for test in self.tests:
            test.run(result)
    
if __name__ == '__main__':
    suite = TestSuite()
    suite.add(TestCaseTest("testTemplateMethod"))
    suite.add(TestCaseTest("testResult"))
    suite.add(TestCaseTest("testFailedResult"))
    suite.add(TestCaseTest("testFailedResultFormatting"))   
    suite.add(TestCaseTest("testFailedSetUp"))
    suite.add(TestCaseTest("testSuite"))
    result = TestResult()
    suite.run(result)
    print result.summary()

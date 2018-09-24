#!/bin/env python

""" util functions for scraping.py """

import logging
import os
import time

def quote(string):
	""" string -> 'string' """
	return "'" + str(string) + "'"

def mkdir(path):
	""" create directory if not exists """
	if not os.path.isdir(path):
		os.makedirs(path)

def touch(path, content=''):
	""" create new file like a boss """
	dirname = os.path.dirname(path)
	if not dirname:
		dirname = os.getcwd()

	mkdir(dirname)
	with open(path, 'w') as file:
		file.write(content)

def read(filename, type='list'):
	""" read content from a file
	return a list, set or dict depend on the value in parameter type
	"""
	if not os.path.isfile(filename):
		touch(filename)

	with open(filename, 'r') as file:
		words = file.readlines()

	if type == 'dict':
		return {word.strip(): None for word in words}
	elif type == 'set':
		return {word.strip() for word in words}
	return [word.strip() for word in words] # list

def put(line, filename):
	""" append a line to filename """
	if not os.path.isfile(filename):
		touch(filename)

	with open(filename, 'a') as file:
		file.write(line + '\n')

def settup_logger(name, logfile, level=logging.INFO):
	""" Setup logger. Usage:

	LOG = settup_logger('info logging', 'scraping.log', level=logging.INFO)

	LOG.info('info message')
	LOG.debug('debug message')
	"""
	if not os.path.isfile(logfile):
		touch(logfile)

	formatter = logging.Formatter(
			'[%(asctime)s] %(lineno)-3d %(filename)-10s %(levelname)-8s %(message)s')

	handler = logging.FileHandler(logfile)
	handler.setFormatter(formatter)

	logger = logging.getLogger(name)
	logger.setLevel(level)
	logger.addHandler(handler)

	return logger

def timer(function):
	""" decorator to time function call
	@timer
	def test():
		for _ in range(0, 10000):
			pass

	test()
	test.elapsed
	"""
	def time_function_call(*args, **kwargs):
		""" get execution time of function call """
		start = time.time()
		function_result = function(*args, **kwargs)
		end = time.time()
		time_function_call.elapsed = end - start

		return function_result

	return time_function_call

# vim: nofoldenable
